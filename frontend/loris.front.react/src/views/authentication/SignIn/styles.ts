import { makeStyles } from '@material-ui/core/styles';

export default makeStyles(theme => ({
  root: {
    height: '100vh',
    overflow: 'hidden',
  },
  image: {
    backgroundImage: 'url(https://source.unsplash.com/collection/80865894)',
    backgroundRepeat: 'no-repeat',
    backgroundColor:
      theme.palette.type === 'light' ? theme.palette.grey[50] : theme.palette.grey[900],
    backgroundSize: 'cover',
    backgroundPosition: 'center',
  },
  paper: {
    margin: theme.spacing(8, 4),
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.primary.main,
  },
  form: {
    width: '100%', // Fix IE 11 issue.
    marginTop: theme.spacing(1),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
  config: {
    display: 'flex',
    flexDirection: 'row',
    alignItems: 'center',
  },
  flagBox: {
    display: 'inline-flex',
    flex: '1',
    height: '35px',
    alignItems: 'center',
    justifyContent: 'center',
    minWidth: '100px',
    maxWidth: '100px',
    flexWrap: 'nowrap',
    flexDirection: 'row',
    padding: '0px',
    margin: '0px',
    backgroundColor: theme.palette.grey[300],
    borderRadius: '5px',
    '& Button': {
      padding: '2px',
      margin: '0px',
    },
  },
}));
