import i18n from '../i18n/config';

const optionsLanguage = [
  {
    value: 1,
    label: i18n.t('lbl_lang_portuguese'),
  },
  {
    value: 2,
    label: i18n.t('lbl_lang_english'),
  },
  {
    value: 3,
    label: i18n.t('lbl_lang_spanish'),
  },
];

const getLanguageDescription = (code: number): string => {
  const found = optionsLanguage.find(e => e.value === code);

  if (found) {
    return found.label;
  }
  return '';
};

export { getLanguageDescription, optionsLanguage };
