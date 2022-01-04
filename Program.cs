using System;

namespace TvShows
{
    class Program
    {

        static TvShowRepository repository = new TvShowRepository();

        static void Main(string[] args)
        {

			//TODO: extract to a method to reduce the main call
            string userOption = GetUserOption();

			while (userOption.ToUpper() != "X")
			{
				switch (userOption)
				{
					case "1":
						ListTvShows();
						break;
					case "2":
						InsertTvShow();
						break;
					case "3":
						UpdateTvShow();
						break;
					case "4":
						DeleteTvShow();
						break;
					case "5":
						VisualizeTvShow();
						break;
					case "C":
					    //TODO: fix this issue
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				userOption = GetUserOption();
			}
        }

        private static void ListTvShows(){
            Console.WriteLine("List TvShows");

            var list = repository.List();

            if (list.Count == 0){
                Console.WriteLine("There are no TvShows registered.");
				//TODO: user want to add a TvShow from here.
                return;
            }

            foreach (var tvshow in list)
            {
                var deletedTvShow = tvshow.returnDeleted();
                Console.WriteLine("#ID {0}: - {1} *{2}*", tvshow.returnId(), tvshow.returnTitle(), (deletedTvShow ? "Deleted" : ""));
            }
        }

		private static void VisualizeTvShow(){
			Console.WriteLine("Visualize a TvShow");

			Console.WriteLine("Enter the ID of a TvShow you want to visualize: ");
			int visualizeId = int.Parse(Console.ReadLine());

			var visualizeTvShow = repository.ReturnById(visualizeId);
			Console.WriteLine(visualizeTvShow);
		}

		private static void DeleteTvShow(){
			Console.WriteLine("Delete a TvShow");

			//TODO: confirmacao
			Console.Write("Enter the ID of a TvShow you want to update: ");
			int deleteId = int.Parse(Console.ReadLine());

			repository.Delete(deleteId);
		}

		private static void UpdateTvShow(){
            Console.WriteLine("Update a TvShow");

			Console.Write("Enter the ID of a TvShow you want to update: ");
			int updatedId = int.Parse(Console.ReadLine());

			//TODO: extract to a method to reuse code:
			foreach (int i in Enum.GetValues(typeof(Gender)))
			{
				Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Gender), i));
			}
            Console.WriteLine("");
			Console.Write("Select a gender number by the ones showed above: ");
			int updateGender = int.Parse(Console.ReadLine());

			Console.Write("Write the TvShow Title: ");
			string updateTitle = Console.ReadLine();

			Console.Write("Write the TvShow Year: ");
			int updateYear = int.Parse(Console.ReadLine());

			Console.Write("Write the TvShow Description: ");
			string updateDescription = Console.ReadLine();

			TvShow updatedTvShow = new TvShow(id: updatedId,
										gender: (Gender)updateGender,
										title: updateTitle,
										year: updateYear,
										description: updateDescription);
            
			repository.Update(updatedId, updatedTvShow);
        }

        private static void InsertTvShow(){
            Console.WriteLine("Insert new TvShow");

			foreach (int i in Enum.GetValues(typeof(Gender)))
			{
				Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Gender), i));
			}
            Console.WriteLine("");
			Console.Write("Select a gender number by the ones showed above: ");
			int insertGender = int.Parse(Console.ReadLine());

			Console.Write("Write the TvShow Title: ");
			string insertTitle = Console.ReadLine();

			Console.Write("Write the TvShow Year: ");
			int insertYear = int.Parse(Console.ReadLine());

			Console.Write("Write the TvShow Description: ");
			string insertDescription = Console.ReadLine();

			TvShow newTvShow = new TvShow(id: repository.NextId(),
										gender: (Gender)insertGender,
										title: insertTitle,
										year: insertYear,
										description: insertDescription);
            
			repository.Insert(newTvShow);
        }

        private static string GetUserOption()
        {
			//TODO: extract to a method to print menu
            Console.WriteLine();
            Console.WriteLine("Welcome! This is DIO TvShow");
            Console.WriteLine("Select an option:");

            Console.WriteLine("1 - List TvShow");
            Console.WriteLine("2 - Insert TvShow");
            Console.WriteLine("3 - Update TvShow");
            Console.WriteLine("4 - Delete TvShow");
            Console.WriteLine("5 - Visualise TvShow");
            Console.WriteLine("C - Clean Sreen");
            Console.WriteLine("X - Exit");
            Console.WriteLine();

            string userOption = Console.ReadLine().ToUpper();
            Console.WriteLine("");
            return userOption;
        }
    }
}
