import { makeStyles } from '@material-ui/styles';

import { CustomTheme } from '../../styles/theme/theme';

export default makeStyles<CustomTheme>(theme => ({
  pageTitleContainer: {
    display: 'flex',
    justifyContent: 'space-between',
    marginBottom: theme.spacing(4),
    marginTop: theme.spacing(5),
  },
  typo: {
    color: theme.palette.text.hint,
  },
  button: {
    boxShadow: theme.customShadows.widget,
    textTransform: 'none',
    '&:active': {
      boxShadow: theme.customShadows.widgetWide,
    },
  },
}));
