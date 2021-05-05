import React, { createContext, useState, useContext } from 'react';
import { useTranslation } from 'react-i18next';
//import { ptBR, enUS, esES } from '@material-ui/core/locale';

import { TLanguage, IChangeLanguage } from '../common/language.d';

const LanguageContext = createContext<IChangeLanguage>({} as IChangeLanguage);

export const getCode = (language: TLanguage): number => {
  if (language === 'english') {
    return 1;
  } else if (language === 'spanish') {
    return 2;
  }
  return 0;
};

export const getLanguage = (languageCode: string | null = ''): TLanguage => {
  if (languageCode === '1' || languageCode === 'english') {
    return 'english';
  } else if (languageCode === '2' || languageCode === 'spanish') {
    return 'spanish';
  }
  return 'portuguese';
};

export const getLocalization = (language: TLanguage): string => {
  if (language === 'english') {
    return 'enUs';
  } else if (language === 'spanish') {
    return 'esEs';
  }
  return 'ptBr';
};

const LanguageProvider: React.FC = ({ children }) => {
  const { i18n } = useTranslation();
  //const lang = i18n.language;

  const changeLanguage = (languageToChange: TLanguage): void => {
    const local = getLocalization(languageToChange);
    i18n.changeLanguage(local);
  };

  const [language, setLanguage] = useState<TLanguage>(() => {
    const languageStorage = getLanguage(localStorage.getItem('@loris.com.br:actualLanguage'));
    changeLanguage(languageStorage);
    return languageStorage;
  });

  const handleChangeLanguage = (languageToChange: TLanguage) => {
    localStorage.setItem('@loris.com.br:actualLanguage', languageToChange);
    setLanguage(languageToChange);
    changeLanguage(languageToChange);
  };

  return (
    <LanguageContext.Provider value={{ language, handleChangeLanguage }}>
      {children}
    </LanguageContext.Provider>
  );
};

export const useLanguage = (): IChangeLanguage => {
  return useContext(LanguageContext);
};

export default LanguageProvider;
