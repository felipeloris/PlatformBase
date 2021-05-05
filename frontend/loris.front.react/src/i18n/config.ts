import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';

import ptBr from './ptBr.json';
import enUs from './enUs.json';
import esEs from './esEs.json';

const resources = {
  ptBr: { translation: { ...ptBr } },
  enUs: { translation: { ...enUs } },
  esEs: { translation: { ...esEs } },
};

i18n.use(initReactI18next).init({
  resources,
  lng: 'ptBr',
  keySeparator: false,
  interpolation: {
    escapeValue: false,
  },
});

export default i18n;
