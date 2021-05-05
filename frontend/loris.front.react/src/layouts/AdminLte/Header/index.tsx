import React, { useState } from 'react';
import { useDispatch } from 'react-redux';
import {
  AppBar,
  Toolbar,
  IconButton,
  InputBase,
  Menu,
  MenuItem,
  Fab,
  Link,
} from '@material-ui/core';
import {
  Menu as MenuIcon,
  MailOutline as MailIcon,
  NotificationsNone as NotificationsIcon,
  Person as AccountIcon,
  Search as SearchIcon,
  Send as SendIcon,
  ArrowBack as ArrowBackIcon,
  Settings as SettingsIcon,
  People as PeopleIcon,
  NaturePeople as NaturePeopleIcon,
  Receipt as ReceiptIcon,
} from '@material-ui/icons';
import classNames from 'classnames';
import { useTranslation } from 'react-i18next';
import { useSelector } from 'react-redux';

import useStyles from './styles';
import Notification from '../Notification';
import UserAvatar from '../UserAvatar';
import { useLayoutState, useLayoutDispatch, layoutToggleSidebar } from '../context/LayoutContext';
import { logoutRequest } from '../../../store/user/actions';
import Typography from '../../../components/Typography';
import Button from '../../../components/Button';
import Badge from '../../../components/Badge';
import { notifications, messages } from './mock';
import { IUserStateWrapper } from '../../../store/user/user.d';

interface IProps {
  history: any;
}

const Header: React.FC<IProps> = props => {
  const { t } = useTranslation();
  const classes = useStyles();
  const layoutState = useLayoutState();
  const layoutDispatch = useLayoutDispatch();
  const dispatch = useDispatch();
  const [mailMenu, setMailMenu] = useState<any>(null);
  const [notificationsMenu, setNotificationsMenu] = useState<any>(null);
  const [profileMenu, setProfileMenu] = useState<any>(null);
  const [adminMenu, setAdminMenu] = useState<any>(null);
  const [isNotificationsUnread, setIsNotificationsUnread] = useState(true);
  const [isMailsUnread, setIsMailsUnread] = useState(true);
  const [isSearchOpen, setSearchOpen] = useState(false);
  const identification = useSelector<IUserStateWrapper, string>(state => state.user.identification);

  //const preventDefault = (event: React.SyntheticEvent) => event.preventDefault();

  return (
    <AppBar position="fixed" className={classes.appBar}>
      <Toolbar className={classes.toolbar}>
        <IconButton
          color="inherit"
          onClick={() => layoutDispatch(layoutToggleSidebar())}
          className={classNames(classes.headerMenuButtonSandwich, classes.headerMenuButtonCollapse)}
        >
          {layoutState.isSidebarOpened ? (
            <ArrowBackIcon
              classes={{
                root: classNames(classes.headerIcon, classes.headerIconCollapse),
              }}
            />
          ) : (
            <MenuIcon
              classes={{
                root: classNames(classes.headerIcon, classes.headerIconCollapse),
              }}
            />
          )}
        </IconButton>
        <Typography variant="h6" weight="medium" className={classes.logotype}>
          Platform Base
        </Typography>
        <div className={classes.grow} />
        <IconButton
          aria-haspopup="true"
          color="inherit"
          className={classes.headerMenuButton}
          aria-controls="admin-menu"
          onClick={e => setAdminMenu(e.currentTarget)}
        >
          <SettingsIcon classes={{ root: classes.headerIcon }} />
        </IconButton>
        <Menu
          id="admin-menu"
          open={Boolean(adminMenu)}
          anchorEl={adminMenu}
          onClose={() => setAdminMenu(null)}
          className={classes.headerMenu}
          classes={{ paper: classes.profileMenu }}
          disableAutoFocusItem
        >
          <Link href={'/admin/users'} className={classes.linkAdminMenu}>
            <MenuItem className={classNames(classes.profileMenuItem, classes.headerMenuItem)}>
              <PeopleIcon className={classes.profileMenuIcon} /> {t('lbl_users')}
            </MenuItem>
          </Link>
          <Link href={'/admin/roles'} className={classes.linkAdminMenu}>
            <MenuItem className={classNames(classes.profileMenuItem, classes.headerMenuItem)}>
              <NaturePeopleIcon className={classes.profileMenuIcon} /> {t('lbl_roles')}
            </MenuItem>
          </Link>
          <Link href={'/admin/resources'} className={classes.linkAdminMenu}>
            <MenuItem className={classNames(classes.profileMenuItem, classes.headerMenuItem)}>
              <ReceiptIcon className={classes.profileMenuIcon} /> {t('lbl_resources')}
            </MenuItem>
          </Link>
        </Menu>
        <div
          className={classNames(classes.search, {
            [classes.searchFocused]: isSearchOpen,
          })}
        >
          <div
            className={classNames(classes.searchIcon, {
              [classes.searchIconOpened]: isSearchOpen,
            })}
            onClick={() => setSearchOpen(!isSearchOpen)}
          >
            <SearchIcon classes={{ root: classes.headerIcon }} />
          </div>
          <InputBase
            placeholder="Search…"
            classes={{
              root: classes.inputRoot,
              input: classes.inputInput,
            }}
          />
        </div>
        <IconButton
          color="inherit"
          aria-haspopup="true"
          aria-controls="mail-menu"
          onClick={e => {
            setNotificationsMenu(e.currentTarget);
            setIsNotificationsUnread(false);
          }}
          className={classes.headerMenuButton}
        >
          <Badge badgeContent={isNotificationsUnread ? notifications.length : null} color="warning">
            <NotificationsIcon classes={{ root: classes.headerIcon }} />
          </Badge>
        </IconButton>
        <Menu
          id="mail-menu"
          open={Boolean(mailMenu)}
          anchorEl={mailMenu}
          onClose={() => setMailMenu(null)}
          MenuListProps={{ className: classes.headerMenuList }}
          className={classes.headerMenu}
          classes={{ paper: classes.profileMenu }}
          disableAutoFocusItem
        >
          <div className={classes.profileMenuUser}>
            <Typography variant="h4" weight="medium">
              New Messages
            </Typography>
            <Typography className={classes.profileMenuLink} component="a" color="secondary">
              {messages.length} New Messages
            </Typography>
          </div>
          {messages.map(message => (
            <MenuItem key={message.id} className={classes.messageNotification}>
              <div className={classes.messageNotificationSide}>
                <UserAvatar color={message.variant} name={message.name} />
                <Typography size="sm" color="text" colorBrightness="secondary">
                  {message.time}
                </Typography>
              </div>
              <div
                className={classNames(
                  classes.messageNotificationSide,
                  classes.messageNotificationBodySide
                )}
              >
                <Typography weight="medium" gutterBottom>
                  {message.name}
                </Typography>
                <Typography color="text" colorBrightness="secondary">
                  {message.message}
                </Typography>
              </div>
            </MenuItem>
          ))}
          <Fab
            variant="extended"
            color="primary"
            aria-label="Add"
            className={classes.sendMessageButton}
          >
            Send New Message
            <SendIcon className={classes.sendButtonIcon} />
          </Fab>
        </Menu>
        <IconButton
          color="inherit"
          aria-haspopup="true"
          aria-controls="mail-menu"
          onClick={e => {
            setMailMenu(e.currentTarget);
            setIsMailsUnread(false);
          }}
          className={classes.headerMenuButton}
        >
          <Badge badgeContent={isMailsUnread ? messages.length : null} color="secondary">
            <MailIcon classes={{ root: classes.headerIcon }} />
          </Badge>
        </IconButton>
        <Menu
          id="notifications-menu"
          open={Boolean(notificationsMenu)}
          anchorEl={notificationsMenu}
          onClose={() => setNotificationsMenu(null)}
          className={classes.headerMenu}
          disableAutoFocusItem
        >
          {notifications.map(notification => (
            <MenuItem
              key={notification.id}
              onClick={() => setNotificationsMenu(null)}
              className={classes.headerMenuItem}
            >
              <Notification {...notification} typographyVariant="inherit" />
            </MenuItem>
          ))}
        </Menu>
        <IconButton
          aria-haspopup="true"
          color="inherit"
          className={classes.headerMenuButton}
          aria-controls="profile-menu"
          onClick={e => setProfileMenu(e.currentTarget)}
        >
          <AccountIcon classes={{ root: classes.headerIcon }} />
        </IconButton>
        <Menu
          id="profile-menu"
          open={Boolean(profileMenu)}
          anchorEl={profileMenu}
          onClose={() => setProfileMenu(null)}
          className={classes.headerMenu}
          classes={{ paper: classes.profileMenu }}
          disableAutoFocusItem
        >
          <div className={classes.profileMenuUser}>
            <Typography variant="h4" weight="medium">
              {identification}
            </Typography>
          </div>
          <div className={classes.profileMenuUser}>
            <Button
              className={classes.profileMenuLink}
              color="primary"
              onClick={() => dispatch(logoutRequest(props.history))}
            >
              Sign Out
            </Button>
          </div>
        </Menu>
      </Toolbar>
    </AppBar>
  );
};

export default Header;
