import { createGlobalStyle } from 'styled-components';

export default createGlobalStyle`
    * {
        margin: 0;
        padding: 0;
        box-sizing: border-box;
        font-family: 'Roboto', sans-serif;
        font-size: 14px;
    }

    html, body, #root {
        height: 100%;
    }

    button {
        cursor: pointer;
    }
`;
