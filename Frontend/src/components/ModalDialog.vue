<template>
    <div class="modal-backdrop" @click.self="emitClose">
      <div class="modal-content">
        <slot></slot>
        <button @click="emitClose">Close</button>
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
  }
  
  .modal-content {
    background: white;
    padding: 20px;
    border-radius: 10px;
  }
  </style>
  