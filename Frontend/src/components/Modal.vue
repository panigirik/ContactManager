<template>
  <div class="modal-backdrop" @click.self="emitClose">
    <div class="modal-content">
      <slot></slot>
      <button @click="emitClose">Закрыть</button>
    </div>
  </div>
</template>

<script setup>
const emit = defineEmits(['close'])

function emitClose() {
  emit('close')
}
</script>

<style scoped>
.modal-backdrop {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(0, 0, 0, 0.3);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
}

.modal-content {
  background: white;
  padding: 20px;
  border-radius: 10px;
  min-width: 300px;
  text-align: center;
}

.modal-content button {
  margin-top: 15px;
  padding: 8px 12px;
  background-color: #2d8cf0;
  color: white;
  border: none;
  border-radius: 6px;
  cursor: pointer;
}

.modal-content button:hover {
  background-color: #1b6fc2;
}
</style>
