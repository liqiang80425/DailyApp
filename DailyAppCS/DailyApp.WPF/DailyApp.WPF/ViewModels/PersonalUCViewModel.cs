using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace DailyApp.WPF.ViewModels
{
    internal class PersonalUCViewModel:BindableBase
    {
        #region 设置主题背景
        private bool _isDarkTheme;
        public bool IsDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                if (SetProperty(ref _isDarkTheme, value))
                {
                    ModifyTheme(theme => theme.SetBaseTheme(value ? Theme.Dark : Theme.Light));
                }
            }
        }

        private static void ModifyTheme(Action<ITheme> modificationAction)
        {
            var paletteHelper = new PaletteHelper();
            ITheme theme = paletteHelper.GetTheme();

            modificationAction?.Invoke(theme);

            paletteHelper.SetTheme(theme);
        }
        #endregion

        public PersonalUCViewModel()
        {
            ChangeHueCommand = new  DelegateCommand<object>(ChangeHue);
        }
        #region 顶部背景颜色

        private readonly PaletteHelper paletteHelper = new PaletteHelper();

        public DelegateCommand<object> ChangeHueCommand { get; }

        public IEnumerable<ISwatch> Swatches { get; } = SwatchHelper.Swatches;

        private void ChangeHue(object? obj)
        {
            ITheme theme = paletteHelper.GetTheme();

            var color = (Color)obj;
            theme.PrimaryLight = new ColorPair(color.Lighten());
            theme.PrimaryMid = new ColorPair(color);
            theme.PrimaryDark = new ColorPair(color.Darken());

            paletteHelper.SetTheme(theme);
        }
        #endregion
    }
}
