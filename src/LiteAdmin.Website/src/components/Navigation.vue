<template>
    <div class="navigation">
        <ul class="navigation__list">
            <li class="navigation__item">
                <router-link to="/">Home</router-link>
            </li>
            <li class="navigation__item" v-for="table in tables">
                <router-link :to="'/maintain/' + table.name" class="navigation__link">{{table.name}}</router-link>
            </li>
        </ul>
    </div>
</template>

<script lang="ts" src="./Navigation.ts"></script>
<style lang="scss" src="./Navigation.scss"></style>
