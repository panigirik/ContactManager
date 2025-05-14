module.exports = {
    root: true,
    env: {
      node: true
    },
    extends: [
      'plugin:vue/vue3-essential',
      'eslint:recommended'
    ],
    parser: 'vue-eslint-parser',
    parserOptions: {
      parser: '@babel/eslint-parser',
      requireConfigFile: false,
      babelOptions: {
        presets: ['@babel/preset-env']
      }
    },
    rules: {
      'vue/multi-word-component-names': 'off', // отключаем правило на имя компонента
      'no-undef': 'off' // отключаем ошибку на defineProps и defineEmits
    }
  }
  