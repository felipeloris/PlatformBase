import { colors } from '@material-ui/core';
import { PaletteOptions } from '@material-ui/core/styles/createPalette';

//const white = '#FFFFFF';
//const black = '#000000';

const contrastText = colors.grey[200];

const palette = (): PaletteOptions => {
  return {
    type: 'dark',
    primary: {
      contrastText: contrastText,
      dark: colors.blue[900],
      main: colors.blue['A400'],
      light: colors.blue['A400'],
    },
    secondary: {
      contrastText: contrastText,
      dark: colors.blue[900],
      main: colors.blue['A400'],
      light: colors.blue['A400'],
    },
    success: {
      contrastText: contrastText,
      dark: colors.green[900],
      main: colors.green[600],
      light: colors.green[400],
    },
    info: {
      contrastText: contrastText,
      dark: colors.blue[900],
      main: colors.blue[600],
      light: colors.blue[400],
    },
    warning: {
      contrastText: contrastText,
      dark: colors.orange[900],
      main: colors.orange[600],
      light: colors.orange[400],
    },
    error: {
      contrastText: contrastText,
      dark: colors.red[900],
      main: colors.red[600],
      light: colors.red[400],
    },
    text: {
      primary: colors.blue[100],
      secondary: colors.blue[400],
    },
    background: {
      default: colors.grey[400],
      paper: colors.grey[900],
    },
    divider: colors.grey[200],
  };
};

export default palette;
