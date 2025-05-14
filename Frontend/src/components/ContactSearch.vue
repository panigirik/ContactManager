<template>
  <div class="container">
    <h2>Поиск контактов</h2>

    <div class="search-bar">
      <input
        v-model="searchQuery"
        type="text"
        placeholder="Введите номер телефона"
        @keyup.enter="searchContact"
      />
      <button @click="searchContact" class="search-button">
        🔍
      </button>
    </div>

    <p v-if="validationError" class="validation-error">
      Номер должен содержать ровно 9 цифр.
    </p>

    <!-- Показ блока "Контакт не найден" -->
    <div v-if="showNotFoundToast" class="toast">
      Контакт не найден
    </div>

    <!-- Показ найденных контактов -->
    <div v-if="contacts.length && !showNotFoundToast" class="contact-list-wrapper">
      <div class="contact-list">
        <div
          v-for="contact in contacts"
          :key="contact.contactId"
          :class="['contact-item', { highlight: contact.contactId === highlightedId }]"
        >
          <p><strong>Имя:</strong> {{ contact.ContactName }}</p>
          <p><strong>Телефон:</strong> {{ contact.MobilePhone }}</p>
          <p><strong>Должность:</strong> {{ contact.JobTitle }}</p>
          <p><strong>Дата рождения:</strong> {{ formatDate(contact.BirthDate) }}</p>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import axios from 'axios'

const contacts = ref([])
const searchQuery = ref('')
const highlightedId = ref(null)
const showNotFoundToast = ref(false)
const validationError = ref(false)

const searchContact = async () => {
  const number = searchQuery.value.trim()

  // Валидация: только 9 цифр
  if (!/^\d{9}$/.test(number)) {
    validationError.value = true
    return
  }

  validationError.value = false
  contacts.value = []
  highlightedId.value = null

  try {
    const encodedNumber = encodeURIComponent(number)
    const res = await axios.get(`http://localhost:5227/api/Contact/number/${encodedNumber}`)

    const contact = res.data

    if (!contact || Object.keys(contact).length === 0) {
      showNotFound()
      return
    }

    contacts.value.push(contact)
    highlightedId.value = contact.contactId

    setTimeout(() => (highlightedId.value = null), 3000)
  } catch (error) {
    if (error.response?.status === 404) {
      showNotFound()
    } else {
      console.error('Ошибка при поиске контакта:', error)
    }
  }
}

const showNotFound = () => {
  showNotFoundToast.value = true
  setTimeout(() => {
    showNotFoundToast.value = false
  }, 2000)
}

const formatDate = (date) => {
  if (!date) return ''
  return new Date(date).toLocaleDateString()
}
</script>

<style scoped>
.container {
  padding: 20px;
  max-width: 600px;
  margin: auto;
}

.search-bar {
  display: flex;
  gap: 10px;
  margin-bottom: 10px;
}

.search-bar input {
  flex: 1;
  padding: 8px;
  font-size: 16px;
}

.search-button {
  padding: 8px 12px;
  font-size: 18px;
  cursor: pointer;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
}

.validation-error {
  color: red;
  margin-bottom: 10px;
}

.contact-list-wrapper {
  margin-top: 20px;
}

.contact-item {
  border: 1px solid #ccc;
  padding: 12px;
  margin-bottom: 10px;
  border-radius: 4px;
  transition: background-color 0.3s;
}

.highlight {
  background-color: #d3f9d8;
}

.toast {
  margin-top: 20px;
  background-color: #ff4d4f;
  color: white;
  padding: 12px 20px;
  border-radius: 6px;
  animation: fadeOut 2s forwards;
  text-align: center;
}

@keyframes fadeOut {
  0% {
    opacity: 1;
  }
  90% {
    opacity: 1;
  }
  100% {
    opacity: 0;
  }
}
</style>
