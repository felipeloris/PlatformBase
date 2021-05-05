import { makeStyles } from '@material-ui/styles';
import { Theme } from '@material-ui/core';

export default makeStyles<Theme>(theme => ({
  root: {
    display: 'flex',
    maxWidth: '100vw',
    overflowX: 'hidden',
  },
  content: {
    //backgroundColor: '#00ff00',
    flexGrow: 1,
    padding: theme.spacing(3),
    width: `calc(100vw - 240px)`,
    minHeight: '100vh',
  },
  contentShift: {
    width: `calc(100vw - ${240 + theme.spacing(6)}px)`,
    transition: theme.transitions.create(['width', 'margin'], {
      easing: theme.transitions.easing.sharp,
      duration: theme.transitions.duration.enteringScreen,
    }),
  },
  fakeToolbar: {
    ...theme.mixins.toolbar,
  },
  link: {
    '&:not(:first-child)': {
      paddingLeft: 15,
    },
  },
  childrenContent: {
    //backgroundColor: '#00ff00',
    minHeight: '86vh',
    margin: '0 auto -40px',
  },
  push: {
    height: '10px',
  },
}));
