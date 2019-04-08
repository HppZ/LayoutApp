using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LayoutApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            Loaded += MainPage_Loaded;
        }

        public static readonly DependencyProperty ItemWidth1Property = DependencyProperty.Register(
            "ItemWidth1", typeof(double), typeof(MainPage), new PropertyMetadata(default(double)));

        public double ItemWidth1
        {
            get { return (double)GetValue(ItemWidth1Property); }
            set { SetValue(ItemWidth1Property, value); }
        }

        public static readonly DependencyProperty ItemWidth2Property = DependencyProperty.Register(
            "ItemWidth2", typeof(double), typeof(MainPage), new PropertyMetadata(default(double)));

        public double ItemWidth2
        {
            get { return (double)GetValue(ItemWidth2Property); }
            set { SetValue(ItemWidth2Property, value); }
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var r = new List<A>();

            var a1 = new A()
            {
                Key = "folders",
            };
            for (int i = 0; i < 5; i++)
            {
                a1.Add(new B1()
                {
                    Name = "folder " + i
                });
            }
            r.Add(a1);

            for (int index = 0; index < 10; index++)
            {
                var a = new A()
                {
                    Key = index.ToString(),
                };
                for (int i = 0; i < 5; i++)
                {
                    a.Add(new B2()
                    {
                        Name = "photo " + i
                    });
                }
                r.Add(a);
            }

            CollectionViewSource.Source = r;
        }

        private void ItemsPanel_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var w = e.NewSize.Width;

            if (w > 1000)
            {
                w -= 20;
                ItemWidth1 = w / 4; // 4 folders per row
                ItemWidth2 = w / 5; // 5 photos per row
            }
            else
            {
                w -= 20;
                ItemWidth1 = w / 3;
                ItemWidth2 = w / 4;
            }

            Debug.WriteLine($"{ItemWidth1} {ItemWidth2}");
        }
    }

    class A : List<B>
    {
        public string Key { get; set; }
    }

    class B
    {

    }

    class B1 : B
    {
        public string Name { get; set; }
    }

    class B2 : B
    {
        public string Name { get; set; }
    }

    class containerStyleSelector : StyleSelector
    {
        public Style Style1 { get; set; }
        public Style Style2 { get; set; }

        protected override Style SelectStyleCore(object item, DependencyObject container)
        {
            if (item is B1)
                return Style1;

            return Style2;
        }
    }
}
