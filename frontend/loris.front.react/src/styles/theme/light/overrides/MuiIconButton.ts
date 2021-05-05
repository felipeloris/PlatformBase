import { default as Palette } from '../palette';

const MuiIconButton = (): any => {
  const palette = Palette();

  return {
    root: {
      color: palette.primary,
      '&:hover': {
        backgroundColor: 'rgba(0, 0, 0, 0.03)',
      },
    },
  };
};

export default MuiIconButton;
