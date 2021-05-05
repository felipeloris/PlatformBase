import React from 'react';
import { Box, IconButton, Link } from '@material-ui/core';
import FacebookIcon from '@material-ui/icons/Facebook';
import TwitterIcon from '@material-ui/icons/Twitter';
import GitHubIcon from '@material-ui/icons/GitHub';

import useStyles from './styles';
import Copyright from '../../../components/Copyright';

const Footer: React.FC = () => {
  const classes = useStyles();

  return (
    <Box mt={5} component="footer" className={classes.footer}>
      <Copyright />
      <Box p={1}>
        <Link href={'https://www.facebook.com/'} target={'_blank'} className={classes.link}>
          <IconButton aria-label="facebook">
            <FacebookIcon fontSize="small" style={{ color: '#6E6E6E99' }} />
          </IconButton>
        </Link>
      </Box>
      <Box p={1}>
        <Link href={'https://twitter.com/'} target={'_blank'} className={classes.link}>
          <IconButton aria-label="twitter">
            <TwitterIcon fontSize="small" style={{ color: '#6E6E6E99' }} />
          </IconButton>
        </Link>
      </Box>
      <Box p={1}>
        <Link href={'https://github.com/'} target={'_blank'} className={classes.link}>
          <IconButton aria-label="github">
            <GitHubIcon fontSize="small" style={{ color: '#6E6E6E99' }} />
          </IconButton>
        </Link>
      </Box>
    </Box>
  );
};

export default Footer;
