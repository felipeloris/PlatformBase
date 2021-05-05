import { createMuiTheme, Theme } from '@material-ui/core';
import createPalette from '@material-ui/core/styles/createPalette';

/*
import * as createPalette from '@material-ui/core/styles/createPalette';
declare module '@material-ui/core/styles/createPalette' {
    interface PaletteOptions {
        success?: PaletteColorOptions;
        warning?: PaletteColorOptions;
    }
}
*/

type Modify<T, R> = Omit<T, keyof R> & R;

export interface CustomShadows  {
  widget: string;
  widgetDark: string;
  widgetWide: string;
}

export type CustomTheme = Modify<
  Theme,
  {
    customShadows: CustomShadows;
  }
>;
