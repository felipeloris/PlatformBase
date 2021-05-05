import React from 'react';
import { useTheme } from '@material-ui/core/styles';
import classnames from 'classnames';

import useStyles from './styles';

type TDotSize = 'large' | 'small';

interface IProps {
  size: TDotSize;
  color: string;
}

const Dot: React.FC<IProps> = ({ size, color }) => {
  const classes = useStyles();
  const theme = useTheme();

  return (
    <div
      className={classnames(classes.dotBase, {
        [classes.dotLarge]: size === 'large',
        [classes.dotSmall]: size === 'small',
      })}
      style={{
        backgroundColor: color && theme.palette[color] && theme.palette[color].main,
      }}
    />
  );
};

export default Dot;
