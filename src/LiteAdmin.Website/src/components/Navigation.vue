<template>
    <ul id="nav">
        <li><router-link to="/">Home</router-link></li>
        <li v-for="table in tables"><router-link :to="'/tables/' + table.name">{{table.name}}</router-link></li>
    </ul>
</template>

<script lang="ts" src="./Navigation.ts"></script>
<style lang="scss" src="./Navigation.scss"></style>
