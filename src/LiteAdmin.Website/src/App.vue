<template>
    <div id="app">
        <div class="page-container">
            <md-app>
                <md-app-toolbar class="md-primary">
                    <div class="md-toolbar-row">
                        <div class="md-toolbar-section-start">
                            <router-link to="/" class="md-title">
                                <img src="./assets/logo.svg" alt="LiteAdmin" class="logo" />
                                LiteAdmin
                            </router-link>
                        </div>
                        <div class="md-toolbar-section-end">
                            <md-button class="md-icon-button" @click="toggleMenu">
                                <md-icon>menu</md-icon>
                            </md-button>
                        </div>
                    </div>
                </md-app-toolbar>

                <md-app-drawer md-persistent="full" :md-active.sync="showMenu">
                    <md-toolbar class="md-transparent" md-elevation="0">
                        <span>Navigation</span>

                        <div class="md-toolbar-section-end">
                            <md-button class="md-icon-button md-dense" @click="toggleMenu">
                                <md-icon>keyboard_arrow_left</md-icon>
                            </md-button>
                        </div>
                    </md-toolbar>

                    <md-list>
                        <md-list-item>
                            <router-link to="/" class="md-list-item-text">Home</router-link>
                        </md-list-item>

                        <md-list-item v-for="table in tables"
                                      :key="table.name">
                            <router-link :to="'/maintain/' + table.name" class="md-list-item-text">{{table.name}}</router-link>
                        </md-list-item>
                    </md-list>

                </md-app-drawer>

                <md-app-content>
                    <router-view />
                </md-app-content>
            </md-app>
        </div>
        <md-snackbar md-position="left" :md-duration="Infinity" :md-active.sync="showSnackbar" md-persistent>
            <span>{{snackbarMessage}}</span>
        </md-snackbar>
    </div>
</template>

<script lang="ts" src="./App.ts"></script>
<style lang="scss" src="./App.scss"></style>
