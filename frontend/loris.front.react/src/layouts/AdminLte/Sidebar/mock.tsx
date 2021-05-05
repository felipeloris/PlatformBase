import React from 'react';
import {
  Home as HomeIcon,
  NotificationsNone as NotificationsIcon,
  FormatSize as TypographyIcon,
  FilterNone as UIElementsIcon,
  BorderAll as TableIcon,
  QuestionAnswer as SupportIcon,
  LibraryBooks as LibraryIcon,
  HelpOutline as FAQIcon,
} from '@material-ui/icons';

const structure = [
  { id: 0, label: 'Dashboard', link: '/app', icon: <HomeIcon /> },
  {
    id: 1,
    label: 'Typography',
    link: '/app/typography',
    icon: <TypographyIcon />,
  },
  { id: 2, label: 'Tables', link: '/app/tables', icon: <TableIcon /> },
  {
    id: 3,
    label: 'Notifications',
    link: '/app/notifications',
    icon: <NotificationsIcon />,
  },
  {
    id: 4,
    label: 'UI Elements',
    link: '/app/ui',
    icon: <UIElementsIcon />,
    children: [
      { label: 'Icons', link: '/app/ui/icons' },
      { label: 'Charts', link: '/app/ui/charts' },
      { label: 'Maps', link: '/app/ui/maps' },
    ],
  },
  { id: 5, type: 'divider' },
  { id: 6, type: 'title', label: 'HELP' },
  { id: 7, label: 'Library', link: 'https://loris.com.br', icon: <LibraryIcon /> },
  { id: 8, label: 'Support', link: 'https://loris.com.br', icon: <SupportIcon /> },
  { id: 9, label: 'FAQ', link: 'https://loris.com.br', icon: <FAQIcon /> },
  { id: 10, type: 'divider' },
];

export default structure;
