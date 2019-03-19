using Binwell.Controls.FastGrid.FastGrid;
using FastGridSample.Cells;
using FastGridSample.DataObjects;
using Xamarin.Forms;

namespace FastGridSample
{
    public class MainContentPage : ContentPage
    {
        private FastGridView fastGridView;

        public MainContentPage()
        {
            Content = GetPageContent();

            var size = Device.Info.ScaledScreenSize;
            fastGridView.ItemTemplateSelector = new FastGridTemplateSelector(
                new FastGridDataTemplate(typeof(CategoryObject).Name, typeof(CategoryCell), new Size(size.Width, 70)),
                new FastGridDataTemplate(typeof(ProductObject).Name, typeof(ProductCell), new Size(size.Width / 2, 260))
            );

            BindingContext = new MainViewModel();
        }

        private View GetPageContent()
        {
            fastGridView = new FastGridView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IsPullToRefreshEnabled = true,
                RowSpacing = 1,
                ColumnSpacing = 1,
            };

            fastGridView.SetBinding(FastGridView.ItemsSourceProperty, "ItemsSource");
            fastGridView.SetBinding(FastGridView.LoadMoreCommandProperty, "LoadMoreCommand");
            fastGridView.SetBinding(FastGridView.ItemSelectedCommandProperty, "ItemSelectedCommand");
            fastGridView.SetBinding(FastGridView.RefreshCommandProperty, "RefreshCommand");

            ContentView contentView = new ContentView
            {
                BackgroundColor = Color.Transparent,
                HeightRequest = 40,
                Content = new ActivityIndicator()
                {
                    IsRunning = true,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    WidthRequest = 32,
                    HeightRequest = 32
                }
            };

            contentView.SetBinding(IsVisibleProperty, "IsBusy");

            Grid grid = new Grid
            {
                RowSpacing = 2
            };

            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(92.5, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(7.5, GridUnitType.Star) });

            grid.Children.Add(fastGridView, 0, 1, 0, 2);
            grid.Children.Add(contentView, 0, 1, 1, 2);

            return grid;
        }
    }
}
