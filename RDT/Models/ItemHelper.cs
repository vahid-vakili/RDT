using System.Linq;
using System.Windows;

namespace RDT.Models
{
    public class ItemHelper : DependencyObject
    {
        public static readonly DependencyProperty IsCheckedProperty = DependencyProperty.RegisterAttached("IsChecked", typeof(bool?), typeof(ItemHelper), new PropertyMetadata(false, new PropertyChangedCallback(OnIsCheckedPropertyChanged)));
        private static void OnIsCheckedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Category && ((bool?)e.NewValue).HasValue)
                foreach (Env p in (d as Category).Members)
                    ItemHelper.SetIsChecked(p, (bool?)e.NewValue);

            if (d is Env)
            {
                int checked1 = ((d as Env).GetValue(ItemHelper.ParentProperty) as Category).Members.Where(x => ItemHelper.GetIsChecked(x) == true).Count();
                int unchecked1 = ((d as Env).GetValue(ItemHelper.ParentProperty) as Category).Members.Where(x => ItemHelper.GetIsChecked(x) == false).Count();
                if (unchecked1 > 0 && checked1 > 0)
                {
                    ItemHelper.SetIsChecked((d as Env).GetValue(ItemHelper.ParentProperty) as DependencyObject, null);
                    return;
                }
                if (checked1 > 0)
                {
                    ItemHelper.SetIsChecked((d as Env).GetValue(ItemHelper.ParentProperty) as DependencyObject, true);
                    return;
                }
                ItemHelper.SetIsChecked((d as Env).GetValue(ItemHelper.ParentProperty) as DependencyObject, false);
            }
        }
        public static void SetIsChecked(DependencyObject element, bool? IsChecked)
        {
            element.SetValue(ItemHelper.IsCheckedProperty, IsChecked);
        }
        public static bool? GetIsChecked(DependencyObject element)
        {
            return (bool?)element.GetValue(ItemHelper.IsCheckedProperty);
        }

        public static readonly DependencyProperty ParentProperty = DependencyProperty.RegisterAttached("Parent", typeof(object), typeof(ItemHelper));
        public static void SetParent(DependencyObject element, object Parent)
        {
            element.SetValue(ItemHelper.ParentProperty, Parent);
        }
        public static object GetParent(DependencyObject element)
        {
            return (object)element.GetValue(ItemHelper.ParentProperty);
        }
    }
}
