import { default as Palette } from '../palette';

const MuiTableRow = (): any => {
  const palette = Palette();

  return {
    root: {
      '&$selected': {
        backgroundColor: palette.background?.default,
      },
      '&$hover': {
        '&:hover': {
          backgroundColor: palette.background?.default,
        },
      },
    },
  };
};

export default MuiTableRow;
