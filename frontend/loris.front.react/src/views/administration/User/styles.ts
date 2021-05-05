import { makeStyles } from '@material-ui/core/styles';

export default makeStyles(theme => ({
  root: {
    height: '74vh',
    minWidth: '356px',
  },
  title: {
    color: theme.palette.primary.contrastText,
    backgroundColor: theme.palette.primary.main,
    fontSize: '1.2rem',
    height: '40px',
    padding: '10px',
  },
  textField: {
    marginLeft: 'theme.spacing(1)',
    marginRight: theme.spacing(1),
  },
  menu: {
    width: 200,
  },
  gridContainer: {
    //backgroundColor: '#00ff00',
    alignItems: 'center',
    justifyItems: 'center',
  },
}));
