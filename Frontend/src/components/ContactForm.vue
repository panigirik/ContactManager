<template>
  <div class="form-wrapper">
    <form @submit.prevent="submitForm" class="form">
      <label>Имя</label>
      <input
        v-model="form.contactName"
        placeholder="Имя с большой буквы, без пробелов"
        required
      />

      <label>Телефон</label>
      <input
        v-model="form.mobilePhone"
        placeholder="Ровно 9 цифр"
        required
      />

      <label>Должность</label>
      <input v-model="form.jobTitle" required placeholder="Укажите должность" />

      <label>Дата рождения</label>
      <input type="date" v-model="form.birthDate" required />

      <button type="submit">Сохранить</button>
    </form>

    <!-- Уведомление об ошибке -->
    <transition name="fade">
      <div v-if="errorMessage" class="toast error-toast">
        {{ errorMessage }}
      </div>
    </transition>

    <!-- Уведомление об успехе -->
    <transition name="fade">
      <div v-if="showSuccess" class="toast success-toast">
        Контакт успешно сохранён!
      </div>
    </transition>
  </div>
</template>

<script setup>
import { ref, watch, toRaw } from 'vue'

const props = defineProps({ contact: Object })
const emit = defineEmits(['save'])

const form = ref({
  contactName: '',
  mobilePhone: '',
  jobTitle: '',
  birthDate: ''
})

const showSuccess = ref(false)
const errorMessage = ref('')

watch(
  () => props.contact,
  () => {
    form.value = props.contact
      ? { ...props.contact }
      : { contactName: '', mobilePhone: '', jobTitle: '', birthDate: '' }
  },
  { immediate: true }
)

const showError = (message) => {
  errorMessage.value = message
  setTimeout(() => {
    errorMessage.value = ''
  }, 2000)
}

const submitForm = () => {
  if (!/^[A-ZА-Я][a-zа-яA-ZА-Я]*$/.test(form.value.contactName)) {
    showError('Имя должно начинаться с заглавной буквы и не содержать пробелов')
    return
  }

  if (!/^\d{9}$/.test(form.value.mobilePhone)) {
    showError('Телефон должен содержать ровно 9 цифр')
    return
  }

  emit('save', toRaw(form.value))

  showSuccess.value = true
  setTimeout(() => {
    showSuccess.value = false
  }, 3000)
}
</script>

<style scoped>
.form-wrapper {
  position: relative;
  max-width: 500px;
  margin: auto;
  padding: 20px;
}

.form {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

input {
  padding: 8px;
  font-size: 14px;
  border-radius: 4px;
  border: 1px solid #ccc;
}

button {
  padding: 10px;
  font-size: 16px;
  background-color: #007bff;
  border: none;
  color: white;
  border-radius: 4px;
  cursor: pointer;
}

/* Общий стиль для всплывающих сообщений */
.toast {
  position: absolute;
  top: -10px;
  right: 10px;
  padding: 10px 14px;
  border-radius: 6px;
  font-size: 14px;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.15);
  opacity: 0.95;
  z-index: 999;
}

.success-toast {
  background-color: #4caf50;
  color: white;
}

.error-toast {
  background-color: #ff4d4f;
  color: white;
}

/* Анимация появления/исчезновения */
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}
</style>
