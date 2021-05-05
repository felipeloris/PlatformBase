import { makeStyles } from '@material-ui/styles';
import { withStyles } from '@material-ui/core';

/* eslint-disable @typescript-eslint/explicit-module-boundary-types */
const createStyled = (styles: any, options: any = null): any => {
  const Styled = function (props) {
    const { children, ...other } = props;
    return children(other);
  };

  return withStyles(styles, options)(Styled);
};

const useStyles = makeStyles(() => ({
  badge: {
    fontWeight: 600,
    height: 16,
    minWidth: 16,
  },
}));

export { createStyled, useStyles };
