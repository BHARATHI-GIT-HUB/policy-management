/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./src/**/*.{html,ts}"],
  theme: {
    colors: {
      rose: "#D5236B",
      licorice: "#1E050F",
      cultured: "#F5F5F5",
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
