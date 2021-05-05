export type TLanguage = 'portuguese' | 'english' | 'spanish';

export interface IChangeLanguage {
  language: TLanguage;
  handleChangeLanguage(language: TLanguage): any;
}
