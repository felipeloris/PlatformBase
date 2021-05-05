import React from 'react';
import { useTheme } from '@material-ui/styles';
import { Theme } from '@material-ui/core';

import useStyles from './styles';
import Typography from '../../../components/Typography';

interface IProps {
  name: string;
  color: string;
}

const UserAvatar: React.FC<IProps> = ({ color = 'primary', ...props }) => {
  const classes = useStyles();
  const theme = useTheme<Theme>();

  const letters = props.name
    .split(' ')
    .map(word => word[0])
    .join('');

  return (
    <div className={classes.avatar} style={{ backgroundColor: theme.palette[color].main }}>
      <Typography className={classes.text}>{letters}</Typography>
    </div>
  );
};

export default UserAvatar;
