using System;
using FaustVXBase.WP8;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace FaustVXBase.WP8.Controls
{
    public sealed partial class Expander : UserControl
    {
        public Expander()
        {
            InitializeComponent();
            Initialize();
            //DataContext = this;
        }

        private void Initialize()
        {
            Btn0.SetBinding(this, IsExpandedProperty, ToggleButton.IsCheckedProperty, BindingMode.TwoWay, true);

            Brdr.SetBinding(this, BorderBrushProperty, Border.BorderBrushProperty, fallback: new SolidColorBrush(Colors.Gray));

            Brdr.SetBinding(this, IsExpandedProperty, UIElement.VisibilityProperty, BindingMode.OneWay, converter: Converters.BoolToVisibility.Converter);

            Presenter.SetBinding(this, ContentProperty, ContentPresenter.ContentTemplateProperty);
        }

        public bool IsExpanded
        {
            get { return PropDPUtilities.GetValue(this, IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsExpanded.  This enables animation, styling, binding, etc...
        public static readonly PropDP<bool, Expander> IsExpandedProperty =
            PropDP<Expander>.Register("IsExpanded", true);
        //DependencyProperty.Register("IsExpanded", typeof(bool), typeof(Expander), new PropertyMetadata(true));

        //public bool IsExpanded
        //{
        //    get { return (bool)GetValue(IsExpandedProperty); }
        //    set { SetValue(IsExpandedProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for IsExpanded.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty IsExpandedProperty =
        //    DependencyProperty.Register("IsExpanded", typeof(bool), typeof(Expander), new PropertyMetadata(true));

        public string Header
        {
            get { return PropDPUtilities.GetValue(this, HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly PropDP<string, Expander> HeaderProperty =
            PropDP<Expander>.Register("Header", "Expand");
        //DependencyProperty.Register("Header", typeof(string), typeof(Expander), new PropertyMetadata("Expand"));

        //public string Header
        //{
        //    get { return (string)GetValue(HeaderProperty); }
        //    set { SetValue(HeaderProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty HeaderProperty =
        //    DependencyProperty.Register("Header", typeof(string), typeof(Expander), new PropertyMetadata("Expand"));

        public new DataTemplate Content
        {
            get { return PropDPUtilities.GetValue(this, ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        public static readonly new PropDP<DataTemplate, Expander> ContentProperty =
            PropDP<Expander>.Register("Content", new DataTemplate());
        //DependencyProperty.Register("Content", typeof(DataTemplate), typeof(Expander), new PropertyMetadata(new DataTemplate()));

        //public new DataTemplate Content
        //{
        //    get { return (DataTemplate)GetValue(ContentProperty); }
        //    set { SetValue(ContentProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Content.  This enables animation, styling, binding, etc...
        //public new static readonly DependencyProperty ContentProperty =
        //    DependencyProperty.Register("Content", typeof(DataTemplate), typeof(Expander), new PropertyMetadata(new DataTemplate(), (s, e) => (s as Expander).Presenter.Content = (e.NewValue as DataTemplate).LoadContent()));

        public new Brush BorderBrush
        {
            get { return PropDPUtilities.GetValue(this, BorderBrushProperty); }
            set { SetValue(BorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BorderBrush.  This enables animation, styling, binding, etc...
        public static readonly new PropDP<Brush, Expander> BorderBrushProperty =
            PropDP<Expander>.Register<Brush>("BorderBrush", new SolidColorBrush(Windows.UI.Colors.Gray));
        //DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(Expander), new PropertyMetadata(new SolidColorBrush(Windows.UI.Colors.Gray)));

        //public new Brush BorderBrush
        //{
        //    get { return (Brush)GetValue(BorderBrushProperty); }
        //    set { SetValue(BorderBrushProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for BorderBrush.  This enables animation, styling, binding, etc...
        //public new static readonly DependencyProperty BorderBrushProperty =
        //    DependencyProperty.Register("BorderBrush", typeof(Brush), typeof(Expander), new PropertyMetadata(new SolidColorBrush(Windows.UI.Colors.Gray)));
    }
}
