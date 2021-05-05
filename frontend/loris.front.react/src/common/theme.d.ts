import { PaletteType, Theme } from '@material-ui/core';

//type TThemeType = 'dark' | 'light';
export type TThemeType = PaletteType;

export interface IThemeChange {
  themeType: TThemeType;
  handleThemeChange(): any;
}
