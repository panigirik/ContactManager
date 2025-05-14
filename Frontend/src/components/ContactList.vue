<template>
  <div class="container">
    <div class="header">
      <button @click="openForm()">Добавить контакт</button>
    </div>

    <div class="contact-cards">
      <div
        v-for="contact in contacts"
        :key="contact.ContactId"
        class="contact-card"
      >
        <p><strong>Имя:</strong> {{ contact.ContactName }}</p>
        <p><strong>Телефон:</strong> {{ contact.MobilePhone }}</p>
        <p><strong>Должность:</strong> {{ contact.JobTitle }}</p>
        <p><strong>Дата рождения:</strong> {{ formatDate(contact.BirthDate) }}</p>
        <div class="actions">
          <button @click="openForm(contact)">✏️</button>
          <button @click="deleteContact(contact.ContactId)">🗑️</button>
        </div>
      </div>
    </div>

    <Modal v-if="showModal" @close="closeModal">
      <ContactForm :contact="selectedContact" @save="handleSave" />
    </Modal>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue'
import axios from 'axios'
import ContactForm from './ContactForm.vue'
import Modal from './Modal.vue'

const contacts = ref([])
const showModal = ref(false)
const selectedContact = ref(null)

const fetchContacts = async () => {
  try {
    const response = await axios.get('http://localhost:5227/api/Contact')
    contacts.value = response.data
  } catch (error) {
    console.error('Ошибка при загрузке контактов:', error)
  }
}

const openForm = (contact = null) => {
  selectedContact.value = contact ? { ...contact } : null
  showModal.value = true
}

const closeModal = () => {
  showModal.value = false
  selectedContact.value = null
}

const handleSave = async (contactData) => {
  try {
    if (selectedContact.value && selectedContact.value.ContactId) {
      await axios.put('http://localhost:5227/api/Contact', contactData);
    } else {
      await axios.post('http://localhost:5227/api/Contact', contactData);
    }
    closeModal();
    await fetchContacts();
  } catch (error) {
    console.error('Ошибка при сохранении контакта:', error.response?.data || error.message);
  }
};


const deleteContact = async (id) => {
  try {
    if (!id) {
      console.error('Не передан корректный ID для удаления!')
      return
    }
    await axios.delete(`http://localhost:5227/api/Contact?contactId=${id}`, {
      headers: {
        'Content-Type': 'application/json',
        'Cache-Control': 'no-cache'
      }
    })
    await fetchContacts()
  } catch (error) {
    console.error('Ошибка при удалении контакта:', error.response?.data || error.message)
  }
}

const formatDate = (dateString) => {
  if (!dateString) return ''
  return new Date(dateString).toLocaleDateString('ru-RU')
}

onMounted(fetchContacts)
</script>

<style scoped>
.container {
  padding: 20px;
  font-family: Arial, sans-serif;
}

.header {
  display: flex;
  justify-content: flex-end;
  margin-bottom: 20px;
}

button {
  padding: 8px 12px;
  background-color: #2d8cf0;
  color: white;
  border: none;
  border-radius: 6px;
  cursor: pointer;
}

button:hover {
  background-color: #1b6fc2;
}

.contact-cards {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
}

.contact-card {
  border: 1px solid #ddd;
  border-radius: 12px;
  padding: 15px;
  width: 270px;
  background-color: #f9f9f9;
  box-shadow: 1px 1px 10px rgba(0, 0, 0, 0.05);
}

.contact-card p {
  margin: 6px 0;
}

.actions {
  display: flex;
  justify-content: space-between;
  margin-top: 10px;
}
</style>
