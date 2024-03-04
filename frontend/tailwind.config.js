/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    colors: {
      rose: "#D5236B",
      licorice: "#1E050F",
      cultured: "#F5F5F5",
      blue: "#1fb6ff",
      purple: "#7e5bef",
      pink: "#ff49db",
      orange: "#ff7849",
      green: "#13ce66",
      yellow: "#ffc82c",
      "gray-dark": "#273444",
      gray: "#8492a6",
      "gray-light": "#d3dce6",
    },

    extend: {
      margin: {
        auto: "0.5rem",
      },
      fontFamily: {
        SF: ["SF Pro Display", "sans-serif"],
      },
      container: {
        center: true,
      },
    },
  },
  plugins: [],
};
