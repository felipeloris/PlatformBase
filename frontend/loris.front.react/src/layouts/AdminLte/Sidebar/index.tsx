import React, { useState, useEffect } from 'react';
import { Drawer, IconButton, List } from '@material-ui/core';
import { useTheme } from '@material-ui/styles';
import { withRouter } from 'react-router-dom';
import classNames from 'classnames';
import { ArrowBack as ArrowBackIcon } from '@material-ui/icons';

import useStyles from './styles';
import SidebarLink from '../SidebarLink';
import { useLayoutState, useLayoutDispatch, layoutToggleSidebar } from '../context/LayoutContext';
import structure from './mock';
import { CustomTheme } from '../../../styles/theme/theme';

interface IProps {
  history: any;
  location: any;
}

const Sidebar: React.FC<IProps> = props => {
  const classes = useStyles();
  const theme = useTheme<CustomTheme>();
  const { isSidebarOpened } = useLayoutState();
  const layoutDispatch = useLayoutDispatch();
  const [isPermanent, setPermanent] = useState(true);

  function handleWindowWidthChange() {
    const windowWidth = window.innerWidth;
    const breakpointWidth = theme.breakpoints.values.md;
    const isSmallScreen = windowWidth < breakpointWidth;

    if (isSmallScreen && isPermanent) {
      setPermanent(false);
    } else if (!isSmallScreen && !isPermanent) {
      setPermanent(true);
    }
  }

  useEffect(function () {
    window.addEventListener('resize', handleWindowWidthChange);
    handleWindowWidthChange();
    return function cleanup() {
      window.removeEventListener('resize', handleWindowWidthChange);
    };
  });

  return (
    <Drawer
      variant={isPermanent ? 'permanent' : 'temporary'}
      className={classNames(classes.drawer, {
        [classes.drawerOpen]: isSidebarOpened,
        [classes.drawerClose]: !isSidebarOpened,
      })}
      classes={{
        paper: classNames({
          [classes.drawerOpen]: isSidebarOpened,
          [classes.drawerClose]: !isSidebarOpened,
        }),
      }}
      open={isSidebarOpened}
    >
      <div className={classes.toolbar} />
      <div className={classes.mobileBackButton}>
        <IconButton onClick={() => layoutDispatch(layoutToggleSidebar())}>
          <ArrowBackIcon
            classes={{
              root: classNames(classes.headerIcon, classes.headerIconCollapse),
            }}
          />
        </IconButton>
      </div>
      <List className={classes.sidebarList}>
        {structure.map(link => (
          <SidebarLink
            key={link.id}
            location={props.location}
            isSidebarOpened={isSidebarOpened}
            {...link}
          />
        ))}
      </List>
    </Drawer>
  );
};

export default withRouter(Sidebar);
