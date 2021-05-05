import React from 'react';
import { MuiThemeProvider } from '@material-ui/core/styles';

import { TThemeType, IThemeChange } from '../common/theme.d';
import theme from '../styles/theme';

const ThemeChangeContext = React.createContext<IThemeChange>({} as IThemeChange);

export const isLight = (themeType: TThemeType): boolean => {
  if (themeType === 'light') {
    return true;
  }
  return false;
};

const ThemeProvider: React.FC = ({ children }) => {
  const [themeType, setThemeType] = React.useState<TThemeType>(() => {
    let actualTheme: TThemeType = 'light';

    if (localStorage.getItem('@loris.com.br:actualTheme') === 'dark') {
      actualTheme = 'dark';
    }

    return actualTheme;
  });

  const memorizedTheme = React.useMemo(() => {
    const newTheme = theme(themeType);

    return newTheme;
  }, [themeType]);

  const handleThemeChange = () => {
    let newThemeType: TThemeType = 'light';

    if (isLight(themeType)) {
      newThemeType = 'dark';
    }

    localStorage.setItem('@loris.com.br:actualTheme', newThemeType);

    setThemeType(newThemeType);
  };

  return (
    <MuiThemeProvider theme={memorizedTheme}>
      <ThemeChangeContext.Provider value={{ themeType, handleThemeChange }}>
        {children}
      </ThemeChangeContext.Provider>
    </MuiThemeProvider>
  );
};

export const useChangeTheme = (): IThemeChange => {
  const context = React.useContext(ThemeChangeContext);
  if (context === undefined) {
    throw new Error('useChangeTheme must be used!');
  }
  return context;
};

export default ThemeProvider;
