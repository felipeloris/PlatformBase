import { TypographyOptions } from '@material-ui/core/styles/createTypography';

import { default as Palette } from './palette';

const typography = (): TypographyOptions => {
  const palette = Palette();

  return {
    h1: {
      color: palette.text?.primary,
      fontWeight: 500,
      fontSize: '39px',
      letterSpacing: '-0.24px',
      lineHeight: '40px',
    },
    h2: {
      color: palette.text?.primary,
      fontWeight: 500,
      fontSize: '33px',
      letterSpacing: '-0.24px',
      lineHeight: '36px',
    },
    h3: {
      color: palette.text?.primary,
      fontWeight: 500,
      fontSize: '29px',
      letterSpacing: '-0.06px',
      lineHeight: '32px',
    },
    h4: {
      color: palette.text?.primary,
      fontWeight: 500,
      fontSize: '24px',
      letterSpacing: '-0.06px',
      lineHeight: '28px',
    },
    h5: {
      color: palette.text?.primary,
      fontWeight: 500,
      fontSize: '20px',
      letterSpacing: '-0.05px',
      lineHeight: '24px',
    },
    h6: {
      color: palette.text?.primary,
      fontWeight: 500,
      fontSize: '18px',
      letterSpacing: '-0.05px',
      lineHeight: '24px',
    },
    subtitle1: {
      color: palette.text?.primary,
      fontSize: '20px',
      letterSpacing: '-0.05px',
      lineHeight: '29px',
    },
    subtitle2: {
      color: palette.text?.secondary,
      fontWeight: 400,
      fontSize: '18px',
      letterSpacing: '-0.05px',
      lineHeight: '25px',
    },
    body1: {
      color: palette.text?.primary,
      fontSize: '17px',
      letterSpacing: '-0.05px',
      lineHeight: '23px',
    },
    body2: {
      color: palette.text?.secondary,
      fontSize: '14px',
      letterSpacing: '-0.04px',
      lineHeight: '20px',
    },
    button: {
      color: palette.text?.primary,
      fontSize: '15px',
    },
    caption: {
      color: palette.text?.secondary,
      fontSize: '15px',
      letterSpacing: '0.33px',
      lineHeight: '17px',
    },
    overline: {
      color: palette.text?.secondary,
      fontSize: '15px',
      fontWeight: 500,
      letterSpacing: '0.33px',
      lineHeight: '17px',
      textTransform: 'uppercase',
    },
  };
};

export default typography;
