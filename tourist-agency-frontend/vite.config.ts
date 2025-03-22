import { defineConfig, loadEnv } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig(({ mode }) => {
  const env = loadEnv(mode, process.cwd(), '');
  return {
    define: {
      'process.env.REACT_APP_CATEGORY_API': JSON.stringify(env.REACT_APP_CATEGORY_API),
      'process.env.REACT_APP_TOURIST_PACKAGES_API': JSON.stringify(env.REACT_APP_TOURIST_PACKAGES_API),
      'process.env.REACT_APP_USER_API': JSON.stringify(env.REACT_APP_USER_API),
      'process.env.REACT_APP_RESERVATION_API': JSON.stringify(env.REACT_APP_RESERVATION_API),
      

    },
    plugins: [react()],
  }
})