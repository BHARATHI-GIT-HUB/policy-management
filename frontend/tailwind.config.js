/** @type {import('tailwindcss').Config} */
const colors = require("tailwindcss/colors");

module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    colors: {
      ...colors,
      rose: "#D5236B",
      licorice: "#1E050F",
      cultured: "#F5F5F5",
    },

    extend: {
      animation: {
        "infinite-scroll": "infinite-scroll 25s linear infinite",
      },
      keyframes: {
        "infinite-scroll": {
          from: { transform: "translateX(0)" },
          to: { transform: "translateX(-100%)" },
        },
      },
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
