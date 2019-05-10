using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace csHomeWork3
{
    public partial class SecondWindow : Window
    {
        public SecondWindow()
        {
            InitializeComponent();

            var users = new List<Models.User>();

            users.Add(new Models.User { Name = "Dave", Password = "1DavePwd" });
            users.Add(new Models.User { Name = "Steve", Password = "2StevePwd" });
            users.Add(new Models.User { Name = "Lisa", Password = "3LisaPwd" });

            uxList.ItemsSource = users;
        }

        GridViewColumnHeader lastClicked = null;
        ListSortDirection lastDirection = ListSortDirection.Ascending;

        void ColumnHeaderClicked(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

                if (headerClicked.Role != GridViewColumnHeaderRole.Padding)
                {
                    if (headerClicked != lastClicked)
                    {
                        direction = ListSortDirection.Ascending;
                    }
                    else
                    {
                        if (lastDirection == ListSortDirection.Ascending)
                        {
                            direction = ListSortDirection.Descending;
                        }
                        else
                        {
                            direction = ListSortDirection.Ascending;
                        }
                    }

                    var columnBinding = headerClicked.Column.DisplayMemberBinding as Binding;
                    var sortBy = columnBinding?.Path.Path ?? headerClicked.Column.Header as string;

                    Sort(sortBy, direction);

                    lastClicked = headerClicked;
                    lastDirection = direction;
                }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            ICollectionView newSort = CollectionViewSource.GetDefaultView(uxList.ItemsSource);

            newSort.SortDescriptions.Clear();
            SortDescription sortDirection = new SortDescription(sortBy, direction);
            newSort.SortDescriptions.Add(sortDirection);
            newSort.Refresh();
        }
    }
}
