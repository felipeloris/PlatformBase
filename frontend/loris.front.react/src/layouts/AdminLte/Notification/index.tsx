import React from 'react';
import { Button } from '@material-ui/core';
import {
  NotificationsNone as NotificationsIcon,
  ThumbUp as ThumbUpIcon,
  ShoppingCart as ShoppingCartIcon,
  LocalOffer as TicketIcon,
  BusinessCenter as DeliveredIcon,
  SmsFailed as FeedbackIcon,
  DiscFull as DiscIcon,
  Email as MessageIcon,
  Report as ReportIcon,
  Error as DefenceIcon,
  AccountBox as CustomerIcon,
  Done as ShippedIcon,
  Publish as UploadIcon,
} from '@material-ui/icons';
import { useTheme } from '@material-ui/styles';
import classnames from 'classnames';
import tinycolor from 'tinycolor2';

import useStyles from './styles';
import { CustomTheme } from '../../../styles/theme/theme';
import Typography from '../../../components/Typography';

const typesIcons = {
  'e-commerce': <ShoppingCartIcon />,
  notification: <NotificationsIcon />,
  offer: <TicketIcon />,
  info: <ThumbUpIcon />,
  message: <MessageIcon />,
  feedback: <FeedbackIcon />,
  customer: <CustomerIcon />,
  shipped: <ShippedIcon />,
  delivered: <DeliveredIcon />,
  defence: <DefenceIcon />,
  report: <ReportIcon />,
  upload: <UploadIcon />,
  disc: <DiscIcon />,
};

function getIconByType(type = 'offer') {
  return typesIcons[type];
}

interface IProps {
  color: string;
  variant?: any;
  type?: any;
  className?: string;
  shadowless?: any;
  typographyVariant?: string;
  message?: string;
  extraButton?: any;
  extraButtonClick?: any;
}

const Notification: React.FC<IProps> = props => {
  const classes = useStyles();
  const theme = useTheme<CustomTheme>();

  const icon = getIconByType(props.type);
  const iconWithStyles = React.cloneElement(icon, {
    classes: {
      root: classes.notificationIconContainer,
    },
    style: {
      color:
        props.variant !== 'contained' &&
        theme.palette[props.color] &&
        theme.palette[props.color].main,
    },
  });

  const sizeOf = (): string => {
    //return props.variant !== 'contained' && !props.typographyVariant && 'md';
    if (props.variant !== 'contained') {
      if (props.typographyVariant) {
        return props.typographyVariant;
      }
      return 'md';
    }
    return '';
  };

  return (
    <div
      className={classnames(classes.notificationContainer, props.className, {
        [classes.notificationContained]: props.variant === 'contained',
        [classes.notificationContainedShadowless]: props.shadowless,
      })}
      style={{
        backgroundColor:
          props.variant === 'contained' &&
          theme.palette[props.color] &&
          theme.palette[props.color].main,
      }}
    >
      <div
        className={classnames(classes.notificationIconContainer, {
          [classes.notificationIconContainerContained]: props.variant === 'contained',
          [classes.notificationIconContainerRounded]: props.variant === 'rounded',
        })}
        style={{
          backgroundColor:
            props.variant === 'rounded' &&
            theme.palette[props.color] &&
            tinycolor(theme.palette[props.color].main).setAlpha(0.15).toRgbString(),
        }}
      >
        {iconWithStyles}
      </div>
      <div className={classes.messageContainer}>
        <Typography
          className={classnames({
            [classes.containedTypography]: props.variant === 'contained',
          })}
          variant={props.typographyVariant}
          size={sizeOf()}
        >
          {props.message}
        </Typography>
        {props.extraButton && props.extraButtonClick && (
          <Button onClick={props.extraButtonClick} disableRipple className={classes.extraButton}>
            {props.extraButton}
          </Button>
        )}
      </div>
    </div>
  );
};

export default Notification;
