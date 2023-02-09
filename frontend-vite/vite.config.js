import mkcert from 'vite-plugin-mkcert';
import {defineConfig} from 'vite';

export default defineConfig({
    server: {https: true},
    plugins: [ mkcert() ],
})
