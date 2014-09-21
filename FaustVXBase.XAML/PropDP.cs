using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FaustVXBase.XAML
{
    public class PropDP<TProperty, TClassOwner>(string property, TProperty defaultValue, PropDP<TProperty, TClassOwner>.PropertyChangedCallback callback)
        where TClassOwner : DependencyObject
    {
        public delegate void PropertyChangedCallback(TClassOwner d, DependencyPropertyChangedEventArgs e);

        public class DependencyPropertyChangedEventArgs(TProperty oldValue, TProperty newValue, DependencyProperty property)
        {
            public enum ConvertState
            {
                ConvertFailed = 0,
                ConvertOk = 1
            }

            public TProperty OldValue { get; } = oldValue;
            public TProperty NewValue { get; } = newValue;
            public object OldObjectValue { get; private set; }
            public object NewObjectValue { get; private set; }
            public DependencyProperty Property { get; } = property;
            public ConvertState State { get; private set; } = ConvertState.ConvertOk;

            public DependencyPropertyChangedEventArgs(object oldValue, object newValue, DependencyProperty property)
                : this((var def = default(TProperty)), def, property)
            {
                State = ConvertState.ConvertFailed;
                OldObjectValue = oldValue;
                NewObjectValue = newValue;
            }
        }

        public string PropertyName { get; } = property;
        public TProperty DefaultValue { get; } = defaultValue;
        public PropertyChangedCallback OriginalCallback { get; } = callback;
        public Windows.UI.Xaml.PropertyChangedCallback Callback { get; } = callback.ConvertCallback();
        public DependencyProperty DependencyProperty { get; } = DependencyProperty.Register(property,
            typeof(TProperty),
            typeof(TClassOwner),
            (callback == null) ? new PropertyMetadata(defaultValue) : new PropertyMetadata(defaultValue, callback.ConvertCallback())
        );

        public PropDP(string property, TProperty defaultValue)
            : this(property, defaultValue, null)
        { }

        public static implicit operator DependencyProperty(PropDP<TProperty, TClassOwner> propDP) => propDP.DependencyProperty;
        public static implicit operator TProperty(PropDP<TProperty, TClassOwner> propDP) => propDP.DefaultValue;
        public static implicit operator string (PropDP<TProperty, TClassOwner> propDP) => propDP.PropertyName;
        public static implicit operator Windows.UI.Xaml.PropertyChangedCallback(PropDP<TProperty, TClassOwner> propDP) => propDP.Callback;

        public override string ToString() => DependencyProperty.ToString();
    }

    public static class PropDP<TClassOwner>
        where TClassOwner : DependencyObject
    {
        public static PropDP<TProperty, TClassOwner> Register<TProperty>(string property, TProperty defaultValue)
        {
            return new PropDP<TProperty, TClassOwner>(property, defaultValue);
        }

        public static PropDP<TProperty, TClassOwner> Register<TProperty>(string property, TProperty defaultValue, PropDP<TProperty, TClassOwner>.PropertyChangedCallback callback)
        {
            return new PropDP<TProperty, TClassOwner>(property, defaultValue, callback);
        }
    }

    public static class PropDPUtilities
    {
        public static PropertyChangedCallback ConvertCallback<TProperty, TClassOwner>(this PropDP<TProperty, TClassOwner>.PropertyChangedCallback callback)
            where TClassOwner : DependencyObject
        {
            return (d, e) => callback(d as TClassOwner, e.ConvertEventArgs<TProperty, TClassOwner>());
        }

        public static PropDP<TProperty, TClassOwner>.DependencyPropertyChangedEventArgs ConvertEventArgs<TProperty, TClassOwner>(this DependencyPropertyChangedEventArgs eventArgs)
            where TClassOwner : DependencyObject
        {
            if (eventArgs.NewValue is TProperty && eventArgs.OldValue is TProperty)
                return new PropDP<TProperty, TClassOwner>.DependencyPropertyChangedEventArgs((TProperty)eventArgs.OldValue, (TProperty)eventArgs.NewValue, eventArgs.Property);
            return new PropDP<TProperty, TClassOwner>.DependencyPropertyChangedEventArgs(eventArgs.OldValue, eventArgs.NewValue, eventArgs.Property);
        }


        public static void ClearValue<TProperty, TClassOwner>(this TClassOwner c, PropDP<TProperty, TClassOwner> dp)
            where TClassOwner : DependencyObject
        {
            c.ClearValue(dp);
        }

        public static void SetValue<TProperty, TClassOwner>(this TClassOwner c, PropDP<TProperty, TClassOwner> dp, TProperty value)
            where TClassOwner : DependencyObject
        {
            c.SetValue(dp, value);
        }

        public static TProperty GetValue<TProperty, TClassOwner>(this TClassOwner c, PropDP<TProperty, TClassOwner> dp)
            where TClassOwner : DependencyObject
        {
            return (TProperty)c.GetValue(dp);
        }

        public static TProperty ReadLocalValue<TProperty, TClassOwner>(this TClassOwner c, PropDP<TProperty, TClassOwner> dp)
            where TClassOwner : DependencyObject
        {
            return (TProperty)c.ReadLocalValue(dp);
        }

        public static TProperty GetAnimationBaseValue<TProperty, TClassOwner>(this TClassOwner c, PropDP<TProperty, TClassOwner> dp)
            where TClassOwner : DependencyObject
        {
            return (TProperty)c.GetAnimationBaseValue(dp);
        }


        public static void SetBinding<TClass, TClassOwner, TProperty>(this TClass element, TClassOwner source, PropDP<TProperty, TClassOwner> path, BindingMode mode, PropDP<TProperty, TClass> dp, TProperty fallback = default(TProperty), IValueConverter converter = null)
            where TClass : FrameworkElement
            where TClassOwner : DependencyObject
        {
            var binding = new Binding()
            {
                Source = source,
                Path = new PropertyPath(path),
                FallbackValue = fallback,
                Mode = mode,
                Converter = converter
            };

            element.SetBinding(dp, binding);
        }

        public static void SetBinding<TClass, TClassOwner, TProperty1, TProperty2>(this TClass element, TClassOwner source, PropDP<TProperty1, TClassOwner> path, BindingMode mode, PropDP<TProperty2, TClass> dp, TProperty1 fallback = default(TProperty1), IValueConverter converter = null)
            where TClass : FrameworkElement
            where TClassOwner : DependencyObject
        {
            var binding = new Binding()
            {
                Source = source,
                Path = new PropertyPath(path),
                FallbackValue = fallback,
                Mode = mode,
                Converter = converter
            };

            element.SetBinding(dp, binding);
        }

        public static void SetBinding<TClass, TClassOwner, TProperty>(this TClass element, TClassOwner source, PropDP<TProperty, TClassOwner> path, BindingMode mode, DependencyProperty dp, TProperty fallback = default(TProperty), IValueConverter converter = null)
            where TClass : FrameworkElement
            where TClassOwner : DependencyObject
        {
            var binding = new Binding()
            {
                Source = source,
                Path = new PropertyPath(path),
                FallbackValue = fallback,
                Mode = mode,
                Converter = converter
            };

            element.SetBinding(dp, binding);
        }
    }
}
