/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./**/*.{razor,html}'],
  theme: {
      extend: {
          scale: {
              '-100': '-1',
          }
      }
  },
  plugins: [],
}

