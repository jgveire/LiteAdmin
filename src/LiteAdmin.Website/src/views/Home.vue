<template>
    <div class="home">
        <md-card md-with-hover 
                 v-for="table in tables"
                 :key="table.name"
                 @click.native="cardClick" 
                 :data-table-name="table.name">
                <md-card-header>
                    <div class="md-title">{{getFriendlyName(table.name)}}</div>
                    <div class="md-subhead">Click here to view the table content of {{getFriendlyName(table.name)}}.</div>
                </md-card-header>
        </md-card>
    </div>
</template>

<script lang="ts" src="./Home.ts"></script>
<style lang="scss" src="./Home.scss"></style>
 