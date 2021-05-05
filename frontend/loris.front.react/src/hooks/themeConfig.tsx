import React from 'react';
import { MuiThemeProvider, ThemeProviderProps } from '@material-ui/core/styles';
import { createMuiTheme, useTheme } from '@material-ui/core/styles';

import { TThemeType } from '../common/theme.d'; //IThemeChange

const ThemeDispatchContext = React.createContext<any>(null);

const ThemeProvider: React.FC<ThemeProviderProps> = ({ children, theme }) => {
  let localTheme: TThemeType = 'light';

  if (localStorage.getItem('@loris.com.br:localTheme') === 'dark') {
    localTheme = 'dark';
  }

  const themeInitialOptions = {
    paletteType: localTheme,
  };

  const [themeOptions, dispatch] = React.useReducer((state: any, action: any) => {
    switch (action.type) {
      case 'changeTheme':
        // eslint-disable-next-line no-case-declarations
        const newPaletteType = action.payload;

        localStorage.setItem('@loris.com.br:localTheme', newPaletteType);

        return {
          ...state,
          paletteType: newPaletteType,
        };
      default:
        throw new Error();
    }
  }, themeInitialOptions);

  const memorizedTheme = React.useMemo(() => {
    console.log(theme);

    return createMuiTheme({
      ...theme,
      palette: { type: themeOptions.paletteType },
    });
  }, [theme, themeOptions.paletteType]);

  return (
    <MuiThemeProvider theme={memorizedTheme}>
      <ThemeDispatchContext.Provider value={dispatch}>{children}</ThemeDispatchContext.Provider>
    </MuiThemeProvider>
  );
};

export default ThemeProvider;

// definir o retorno como 'IThemeChange'
export const useChangeTheme: any = () => {
  const dispatch = React.useContext(ThemeDispatchContext);
  const theme = useTheme();
  const themeType: TThemeType = theme.palette.type;
  const isLight: boolean = themeType === 'light';
  const newThemeType: TThemeType = isLight ? 'dark' : 'light';

  const handleThemeChange: any = React.useCallback(
    () =>
      dispatch({
        type: 'changeTheme',
        payload: newThemeType,
      }),
    [newThemeType, dispatch]
  );

  return { isLight, themeType, handleThemeChange };
};
