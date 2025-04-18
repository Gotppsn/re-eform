module.exports = {
    content: [
      './Pages/**/*.cshtml',
      './Views/**/*.cshtml'
    ],
    theme: {
      extend: {
        colors: {
          primary: {
            50: '#eef2ff',
            100: '#e0e7ff',
            500: '#6366f1',
            600: '#4f46e5',
            700: '#4338ca',
          },
          secondary: {
            500: '#14b8a6',
            600: '#0d9488',
            700: '#0f766e',
          }
        },
        boxShadow: {
          'card': '0 4px 6px -1px rgba(0, 0, 0, 0.1), 0 2px 4px -1px rgba(0, 0, 0, 0.06)',
          'card-hover': '0 10px 15px -3px rgba(0, 0, 0, 0.1), 0 4px 6px -2px rgba(0, 0, 0, 0.05)',
          'form-element': '0 1px 2px 0 rgba(0, 0, 0, 0.05)',
        }
      },
    },
    plugins: [
      require('@tailwindcss/forms'),
    ],
  }