using System;

namespace TvShows
{
    class Program
    {

        static TvShowRepository repository = new TvShowRepository();

        static void Main(string[] args)
        {
            //DONE: extract to a method to reduce the main call
            SelectUserOption();
        }

        private static void SelectUserOption()
        {
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
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }

                userOption = GetUserOption();
            }
        }

        private static void ListTvShows()
        {
            Console.WriteLine("############");
            Console.WriteLine("List TvShows");
            Console.WriteLine("############");

            var list = repository.List();

            if (list.Count == 0)
            {
                Console.WriteLine("There are no TvShows registered.");
                //DONE: user be able to add a TvShow from here.
                Console.Write("Do you want to insert a TvShow now? (y/n): ");
                string addNewTvShow = Console.ReadLine().ToUpper();
                if (addNewTvShow == "Y")
                {
                    Console.WriteLine("");
                    InsertTvShow();
                }
                return;
            }

            foreach (var tvshow in list)
            {
                var deletedTvShow = tvshow.returnDeleted();
                Console.WriteLine("#ID {0}: - #Name: {1} {2}", tvshow.returnId(), tvshow.returnTitle(), (deletedTvShow ? "**Deleted**" : ""));
            }
        }

        private static void VisualizeTvShow()
        {
            Console.WriteLine("##################");
            Console.WriteLine("Visualize a TvShow");
            Console.WriteLine("##################");


            Console.WriteLine("Enter the ID of a TvShow you want to visualize: ");
            int visualizeId = int.Parse(Console.ReadLine());

            var visualizeTvShow = repository.ReturnById(visualizeId);
            Console.WriteLine(visualizeTvShow);
        }

        private static void DeleteTvShow()
        {
            Console.WriteLine("###############");
            Console.WriteLine("Delete a TvShow");
            Console.WriteLine("###############");

            Console.Write("Enter the ID of a TvShow you want to update: ");
            int deleteId = int.Parse(Console.ReadLine());
            var tvShowToDelete = repository.ReturnById(deleteId);
            bool isTvShowDeleted = tvShowToDelete.returnDeleted();

            //DONE: check if is already deleted before delete
            if (isTvShowDeleted)
            {
                Console.WriteLine("The TvShow with ID: {0} is already deleted", deleteId);
                return;
            }

            //DONE: show the whole information about the TvShow before confirm deletion
            Console.WriteLine("Do you really want to delete this TvShow: ");
            Console.WriteLine(tvShowToDelete);
            Console.Write("(y/n): ");
            string deleteTvShow = Console.ReadLine().ToUpper();
            //DONE: Confirm before delete
            if (deleteTvShow == "Y")
            {
                repository.Delete(deleteId);
                Console.Write("TvShow ID: {0} deleted successfully.", deleteId);
                Console.WriteLine("");
                return;
            }
            Console.Write("TvShow ID: {0} not deleted.", deleteId);
            Console.WriteLine("");
            return;
        }

        private static void UpdateTvShow()
        {
            Console.WriteLine("###############");
            Console.WriteLine("Update a TvShow");
            Console.WriteLine("###############");

            Console.Write("Enter the ID of a TvShow you want to update: ");
            int updatedId = int.Parse(Console.ReadLine());

            //DONE: extract to a method to reuse code:
            int updateGender, updateYear;
            string updateTitle, updateDescription;

            SetTvShowInfo(out updateGender, out updateTitle, out updateYear, out updateDescription);

            TvShow updatedTvShow = new TvShow(id: updatedId,
                                        gender: (Gender)updateGender,
                                        title: updateTitle,
                                        year: updateYear,
                                        description: updateDescription);

            repository.Update(updatedId, updatedTvShow);
        }

        private static void InsertTvShow()
        {
            Console.WriteLine("#################");
            Console.WriteLine("Insert new TvShow");
            Console.WriteLine("#################");

            int insertGender, insertYear;
            string insertTitle, insertDescription;
            SetTvShowInfo(out insertGender, out insertTitle, out insertYear, out insertDescription);

            TvShow newTvShow = new TvShow(id: repository.NextId(),
                                        gender: (Gender)insertGender,
                                        title: insertTitle,
                                        year: insertYear,
                                        description: insertDescription);

            repository.Insert(newTvShow);
        }

        private static void SetTvShowInfo(out int gender, out string title, out int year, out string description)
        {
            foreach (int i in Enum.GetValues(typeof(Gender)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Gender), i));
            }
			
            Console.WriteLine("");
            Console.Write("Select a gender number by the ones showed above: ");
            gender = int.Parse(Console.ReadLine());

            Console.Write("Write the TvShow Title: ");
            title = Console.ReadLine();

            Console.Write("Write the TvShow Year: ");
            year = int.Parse(Console.ReadLine());

            Console.Write("Write the TvShow Description: ");
            description = Console.ReadLine();
        }

        private static string GetUserOption()
        {
            //DONE: extract to a method to print menu
            PrintMenu();

            string userOption = Console.ReadLine().ToUpper();
            Console.WriteLine("");
            return userOption;
        }

        private static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("##################");
            Console.WriteLine("This is DIO TvShow");
            Console.WriteLine("##################");
            Console.WriteLine();

            Console.WriteLine("Select an option:");
            Console.WriteLine("1 - List TvShow");
            Console.WriteLine("2 - Insert TvShow");
            Console.WriteLine("3 - Update TvShow");
            Console.WriteLine("4 - Delete TvShow");
            Console.WriteLine("5 - Visualise TvShow");
            Console.WriteLine("C - Clean Sreen");
            Console.WriteLine("X - Exit");
            Console.WriteLine("----------------------");
        }
    }
}
