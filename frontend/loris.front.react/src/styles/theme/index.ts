import { createMuiTheme } from '@material-ui/core';

import { TThemeType } from '../../common/theme';
import { isLight } from '../../hooks/theme';
import { default as paletteLight } from './light/palette';
import { default as typographyLight } from './light/typography';
import { default as paletteDark } from './dark/palette';
import { default as typographyDark } from './dark/typography';
import { CustomTheme } from './theme';

const theme = (themeType: TThemeType): CustomTheme => {
  const palette = isLight(themeType) ? paletteLight() : paletteDark();
  const typography = isLight(themeType) ? typographyLight() : typographyDark();

  const baseTheme = createMuiTheme({
    palette,
    typography,
    //overrides: overrides,
    zIndex: {
      appBar: 1200,
      drawer: 1100,
    },
  });

  return {
    ...baseTheme,
    customShadows: {
      widget: '0px 3px 11px 0px #E8EAFC, 0 3px 3px -2px #B2B2B21A, 0 1px 8px 0 #9A9A9A1A',
      widgetDark: '0px 3px 18px 0px #4558A3B3, 0 3px 3px -2px #B2B2B21A, 0 1px 8px 0 #9A9A9A1A',
      widgetWide: '0px 12px 33px 0px #E8EAFC, 0 3px 3px -2px #B2B2B21A, 0 1px 8px 0 #9A9A9A1A',
    },
  };
};

export default theme;
