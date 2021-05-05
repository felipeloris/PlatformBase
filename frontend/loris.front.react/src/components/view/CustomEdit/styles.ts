import { makeStyles } from '@material-ui/core/styles';

export default makeStyles(theme => ({
  container: {
    //backgroundColor: '#00ff00',
    padding: '10px',
    height: '100%',
  },
  title: {
    color: theme.palette.primary.contrastText,
    backgroundColor: theme.palette.primary.main,
    fontSize: '1.2rem',
    height: '40px',
    padding: '10px',
  },
  commandBox: {
    alignItems: 'flex-start',
    justifyItems: 'flex-start',
    padding: '10px',
  },
  commandButton: {
    color: theme.palette.secondary.main,
  },
}));
