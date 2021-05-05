import React from 'react';

import useStyles from './styles';
import Typography from '../Typography';

interface IProps {
  title: string;
  button: any;
}

const PageTitle: React.FC<IProps> = props => {
  const classes = useStyles();

  return (
    <div className={classes.pageTitleContainer}>
      <Typography className={classes.typo} variant="h1" size="sm">
        {props.title}
      </Typography>
      {props.button && props.button}
    </div>
  );
};

export default PageTitle;
