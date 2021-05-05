import React from 'react';
import IconButton from '@material-ui/core/IconButton';
import { IconFlagBR, IconFlagUS, IconFlagES } from 'material-ui-flags';

import { useLanguage } from '../../hooks/language';
import { TLanguage } from '../../common/language.d';
import If from '../../components/operators/If';
import useStyles from './styles';

interface IFlagProps {
  flagLanguage: TLanguage;
}

const Flag: React.FC<IFlagProps> = ({ flagLanguage }) => {
  const { language, handleChangeLanguage } = useLanguage();
  const classes = useStyles();

  const checkSelected = () => {
    if (flagLanguage === language) {
      return classes.flagSelected;
    }
    return classes.flagNotSelected;
  };

  return (
    <IconButton
      onClick={() => {
        handleChangeLanguage(flagLanguage);
      }}
    >
      <If test={flagLanguage === 'portuguese'}>
        <IconFlagBR className={checkSelected()} />
      </If>
      <If test={flagLanguage === 'english'}>
        <IconFlagUS className={checkSelected()} />
      </If>
      <If test={flagLanguage === 'spanish'}>
        <IconFlagES className={checkSelected()} />
      </If>
    </IconButton>
  );
};

export default Flag;
