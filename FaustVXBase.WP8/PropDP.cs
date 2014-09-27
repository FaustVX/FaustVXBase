using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace FaustVXBase.WP8
{
    public class PropDP<TProperty, TClassOwner>(string property, TProperty defaultValue, PropDP<TProperty, TClassOwner>.PropertyChangedCallback callback)
        where TClassOwner : DependencyObject
    {
        public delegate void PropertyChangedCallback(TClassOwner d, DependencyPropertyChangedEventArgs e);

        public class DependencyPropertyChangedEventArgs(TProperty oldValue, TProperty newValue, DependencyProperty property)
        {
            public enum ConvertState
            {
                ConvertFailed,
                ConvertOk
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


        public static void SetBinding<TClass, TClassOwner, TProperty>(this TClass element, TClassOwner source, PropDP<TProperty, TClassOwner> path, PropDP<TProperty, TClass> dp, BindingMode mode = default(BindingMode), TProperty fallback = default(TProperty))
            where TClass : FrameworkElement
            where TClassOwner : DependencyObject
        {
            var binding = new Binding()
            {
                Source = source,
                Path = new PropertyPath(path),
                FallbackValue = fallback
            };
            if (mode != default(BindingMode))
                binding.Mode = mode;

            element.SetBinding(dp, binding);
        }

        public static void SetBinding<TClass, TClassOwner, TProperty1, TProperty2>(this TClass element, TClassOwner source, PropDP<TProperty1, TClassOwner> path, PropDP<TProperty2, TClass> dp, BindingMode mode = default(BindingMode), TProperty1 fallback = default(TProperty1), ValueConverter<TProperty1, TProperty2> converter = null)
            where TClass : FrameworkElement
            where TClassOwner : DependencyObject
        {
            var binding = new Binding()
            {
                Source = source,
                Path = new PropertyPath(path),
                FallbackValue = fallback,
                Converter = converter
            };
            if (mode != default(BindingMode))
                binding.Mode = mode;

            element.SetBinding(dp, binding);
        }

        public static void SetBinding<TClass, TClassOwner, TProperty>(this TClass element, TClassOwner source, PropDP<TProperty, TClassOwner> path, DependencyProperty dp, BindingMode mode = default(BindingMode), TProperty fallback = default(TProperty), IValueConverter converter = null)
            where TClass : FrameworkElement
            where TClassOwner : DependencyObject
        {
            var binding = new Binding()
            {
                Source = source,
                Path = new PropertyPath(path),
                FallbackValue = fallback,
                Converter = converter
            };
            if (mode != default(BindingMode))
                binding.Mode = mode;

            element.SetBinding(dp, binding);
        }
    }

    //[ValueConversion(typeof(TInput), typeof(TOutput))]
    public abstract class ValueConverter<TInput, TOutput> : IValueConverter
    {
        public abstract TOutput Convert(TInput value, object parameter, string language);

        public abstract TInput ConvertBack(TOutput value, object parameter, string language);

        object IValueConverter.Convert(object value, Type targetType, object parameter, string language) => Convert((TInput)value, parameter, language);

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, string language) => ConvertBack((TOutput)value, parameter, language);
    }

    public abstract class AdvancedNotifyPropertyChanged(PropertyChangedEventHandler defaultEvent) : INotifyPropertyChanged
    {
        public AdvancedNotifyPropertyChanged()
            : this(null)
        { }

        public event PropertyChangedEventHandler PropertyChanged = defaultEvent ?? DefaultEvent;

        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") => PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        private static void DefaultEvent(object sender, PropertyChangedEventArgs e) { }

        protected void RaisePropertyChanged<TProperty>(ref TProperty oldValue, TProperty newValue, [CallerMemberName]string propertyName = "")
        {
            if (oldValue.Equals(newValue))
                return;
            oldValue = newValue;
            OnPropertyChanged(propertyName);
        }
    }
}
