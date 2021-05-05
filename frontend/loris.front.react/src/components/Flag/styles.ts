import { makeStyles } from '@material-ui/core/styles';

export default makeStyles(theme => ({
  flagSelected: {
    boxShadow: '1px 1px 3px ' + theme.palette.primary.main,
    '&:hover': {
      boxShadow: '2px 2px 5px ' + theme.palette.primary.dark,
    },
  },
  flagNotSelected: {
    opacity: '0.5',
    '&:hover': {
      boxShadow: '2px 2px 5px ' + theme.palette.primary.dark,
    },
  },
}));
