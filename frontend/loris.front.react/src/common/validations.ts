/* eslint-disable react-hooks/rules-of-hooks */
/* eslint-disable @typescript-eslint/no-unused-vars */
/* eslint-disable @typescript-eslint/explicit-module-boundary-types */
import i18n from '../i18n/config';

export class AuthenticationValidations {
  _items: Array<ItemError>;

  constructor() {
    this._items = new Array<ItemError>();
  }

  hasError(): boolean {
    return this._items.length !== 0;
  }

  getMessages(): string {
    const messages = this._items.reduce((msg, item) => {
      return ` - ${item.message}`;
    }, '');

    return messages;
  }

  getMessageFirst(): string {
    if (this.hasError()) {
      const item = this._items[0];
      return item.message;
    }
    return '';
  }

  setFocusFirst(): void {
    if (this.hasError()) {
      const item = this._items[0];
      item.reference.current.focus();
    }
  }

  identification(ref: any): boolean {
    const value = ref.current.value;
    if (value.length < 3 || value.length > 60) {
      this._items.push(new ItemError(i18n.t('msg_id_must_be_provided'), ref));
      return false;
    }
    return true;
  }

  key(ref: any): boolean {
    const value = ref.current.value;
    if (value.length < 3 || value.length > 60) {
      //this._items.push(new ItemError(i18n.t(''), ref));
      this._items.push(new ItemError('Chave de segurança deve ser fornecida!', ref));
      return false;
    }
    return true;
  }

  password(ref: any, field: string) {
    const value = ref.current.value;
    if (value.length < 6 || value.length > 15) {
      this._items.push(
        new ItemError(
          `Campo ${field} deve ser preenchido com no mínimo 6 caracteres e no máximo 20`,
          ref
        )
      );
      return false;
    }
  }

  newPassword(refPassword1: any, refPassword2: any) {
    const value1: string = refPassword1.current.value;
    const value2: string = refPassword2.current.value;
    if (value1 !== value2) {
      this._items.push(new ItemError(i18n.t('msg_invalid_pwd_confirm'), refPassword1));
    }
  }
}

class ItemError {
  _message: string;
  _reference: any;

  constructor(message: string, reference: any) {
    this._message = message;
    this._reference = reference;
  }

  get message() {
    return this._message;
  }

  get reference() {
    return this._reference;
  }
}

// todo = criar uma classe de validação genérica, e injetar a classe com os métodos
