import { makeStyles } from '@material-ui/styles';
import { Theme } from '@material-ui/core';

export default makeStyles<Theme>(theme => ({
  footer: {
    all: 'initial',
    height: '35px',
    width: '100%',
    backgroundColor: theme.palette.grey[300],
    display: 'flex',
    flexDirection: 'row',
    alignItems: 'center',
    justifyContent: 'flex-end',
  },
  link: {
    paddingLeft: 15,
    paddingRight: 15,
  },
  link2: {
    '&:not(:first-child)': {
      paddingLeft: 15,
      paddingRight: 15,
    },
  },
}));
